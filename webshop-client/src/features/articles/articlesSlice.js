import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import articlesService from "./articlesService";
import { articleResponseDto } from "../../models/articleDto";

const initialState = {
  articles: [],
  isError: false,
  isSuccess: false,
  isLoading: false,
  message: "",
};

export const getArticles = createAsyncThunk(
  "articles/get",
  async (sellerId, thunkAPI) => {
    try {
      return await articlesService.getArticles(sellerId);
    } catch (error) {
      const message =
        (error.response &&
          error.response.data &&
          error.response.data.message) ||
        error.message ||
        error.toString();

      return thunkAPI.rejectWithValue(message);
    }
  }
);

export const articlesSlice = createSlice({
  name: "articles",
  initialState,
  reducers: {
    resetState: (state) => {
      state.isError = false;
      state.isSuccess = false;
      state.isLoading = false;
      state.message = "";
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(getArticles.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(getArticles.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isSuccess = true;
        state.articles = action.payload.map((data) => articleResponseDto(data));
      })
      .addCase(getArticles.rejected, (state, action) => {
        state.isLoading = false;
        state.isError = true;
        state.message = action.payload;
      });
  },
});

export const { resetState } = articlesSlice.actions;
export default articlesSlice.reducer;
