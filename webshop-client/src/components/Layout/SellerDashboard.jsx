import React, { useEffect } from "react";
import ArticleList from "../Articles/ArticleList";
import ArticleForm from "../Articles/ArticleForm";
import { useDispatch, useSelector } from "react-redux";
import {
  addArticle,
  deleteArticle,
  resetState,
} from "../../features/articles/articlesSlice";
import { articleRequestDto } from "../../models/articleDto";
import { notifySuccess, notifyError } from "../../utils/notify";

const SellerDashboard = ({ children }) => {
  const dispatch = useDispatch();

  const { articles, isSuccess, isLoading, isError, message } = useSelector(
    (state) => state.articles
  );

  useEffect(() => {
    if (isError && message) {
      notifyError(message);
    }

    if (isSuccess && message) {
      notifySuccess(message);
    }

    dispatch(resetState());
  }, [isSuccess, isLoading, isError, message, dispatch]);

  const handleCreate = (data) => {
    const dto = articleRequestDto(data);
    dispatch(addArticle(dto));
  };

  const handleDelete = (id) => {
    dispatch(deleteArticle(id));
  };

  return (
    <div>
      <h1>Seller Dashboard</h1>
      <hr />
      {children}
      <hr />
      <div>
        <h3>My Articles</h3>
        <ArticleList
          articles={articles}
          canDelete={true}
          handleDelete={handleDelete}
        />
      </div>
      <hr />
      <div>
        <h3>Add Article</h3>
        <ArticleForm handleSubmit={handleCreate} />
      </div>
    </div>
  );
};

export default SellerDashboard;
