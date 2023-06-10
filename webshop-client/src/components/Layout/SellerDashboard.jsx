import React, { useEffect, useState } from "react";
import ArticleList from "../Articles/ArticleList";
import ArticleForm from "../Articles/ArticleForm";
import { useDispatch, useSelector } from "react-redux";
import {
  addArticle,
  deleteArticle,
  editArticle,
  resetState,
} from "../../features/articles/articlesSlice";
import { articleRequestDto } from "../../models/articleDto";
import { notifySuccess, notifyError } from "../../utils/notify";
import ArticleModal from "../Articles/ArticleModal";

const SellerDashboard = ({ children }) => {
  const dispatch = useDispatch();

  const [edittingArticle, setEdittingArticle] = useState(null);
  const [showModal, setShowModal] = useState(false);

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
  }, [
    isSuccess,
    isLoading,
    isError,
    message,
    articles,
    edittingArticle,
    dispatch,
  ]);

  const handleCreate = (data) => {
    const dto = articleRequestDto(data);
    dispatch(addArticle(dto));
  };

  const handleDelete = (id) => {
    dispatch(deleteArticle(id));
  };

  const handleSetEdit = (articleData) => {
    setEdittingArticle(articleData);
    setShowModal(true);
  };

  const handleCancelEdit = () => {
    setEdittingArticle(null);
    setShowModal(false);
  };

  const handleConfirmEdit = (articleData) => {
    const data = {
      articleId: edittingArticle.id,
      articleData: articleRequestDto(articleData),
    };

    dispatch(editArticle(data));

    setEdittingArticle(null);
    setShowModal(false);
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
          canEdit={true}
          handleSetEdit={handleSetEdit}
        />
      </div>
      <hr />
      <div>
        <h3>Add Article</h3>
        <ArticleForm handleSubmit={handleCreate} />
      </div>
      <ArticleModal
        isVisible={showModal}
        data={edittingArticle}
        handleClose={handleCancelEdit}
        handleConfirm={handleConfirmEdit}
      />
    </div>
  );
};

export default SellerDashboard;
