import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import authService from "./authService";
import { loginResponseDto } from "../../models/loginDto";
import { userResponseDto } from "../../models/userDto";

const localUserId = localStorage.getItem("userId");
const localAccessToken = localStorage.getItem("accessToken");

const initialState = {
  userId: localUserId ? localUserId : null,
  userInfo: null,
  accessToken: localAccessToken ? localAccessToken : null,
  isError: false,
  isSuccess: false,
  isLoading: false,
  message: "",
};

export const registerUser = createAsyncThunk(
  "auth/register",
  async (userData, thunkAPI) => {
    try {
      return await authService.registerUser(userData);
    } catch (error) {
      const message =
        (error.response &&
          error.response.data &&
          error.response.data.message) ||
        // TODO - After adding error dtos remove next line
        (error.response && error.response.data);
      error.message || error.toString();

      return thunkAPI.rejectWithValue(message);
    }
  }
);

export const loginUser = createAsyncThunk(
  "auth/login",
  async (userData, thunkAPI) => {
    try {
      return await authService.loginUser(userData);
    } catch (error) {
      const message =
        (error.response &&
          error.response.data &&
          error.response.data.message) ||
        // TODO - After adding error dtos remove next line
        (error.response && error.response.data);
      error.message || error.toString();

      return thunkAPI.rejectWithValue(message);
    }
  }
);

export const getUserInfo = createAsyncThunk(
  "auth/getUser",
  async (_, thunkAPI) => {
    try {
      const accessToken = thunkAPI.getState().auth.accessToken;
      const userId = thunkAPI.getState().auth.userId;
      return await authService.getUserInfo(accessToken, userId);
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

export const authSlice = createSlice({
  name: "auth",
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
      .addCase(registerUser.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(registerUser.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isSuccess = true;
        state.message = "User successfully registered!";
      })
      .addCase(registerUser.rejected, (state, action) => {
        state.isLoading = false;
        state.isError = true;
        state.message = action.payload;
      })
      .addCase(loginUser.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(loginUser.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isSuccess = true;
        state.message = "User successfully logged in!";

        const responseDto = loginResponseDto(action.payload);
        state.userId = responseDto.id;
        state.accessToken = responseDto.token;

        localStorage.setItem("userId", state.userId);
        localStorage.setItem("accessToken", state.accessToken);
      })
      .addCase(loginUser.rejected, (state, action) => {
        state.isLoading = false;
        state.isError = true;
        state.message = action.payload;
      })
      .addCase(getUserInfo.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(getUserInfo.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isSuccess = true;

        const responseDto = userResponseDto(action.payload);
        state.userInfo = { ...responseDto };
      })
      .addCase(getUserInfo.rejected, (state, action) => {
        state.isLoading = false;
        state.isError = true;
        state.message = action.payload;
      });
  },
});

export const { resetState } = authSlice.actions;
export default authSlice.reducer;
