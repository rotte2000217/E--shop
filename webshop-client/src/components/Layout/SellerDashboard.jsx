import React from "react";
import ArticleList from "../Articles/ArticleList";
import { useSelector } from "react-redux";

const SellerDashboard = ({ children }) => {
  const { articles } = useSelector((state) => state.articles);

  return (
    <div>
      <h1>Seller Dashboard</h1>
      <hr />
      {children}
      <hr />
      <div>
        <h3>My Articles</h3>
        <ArticleList articles={articles} />
      </div>
    </div>
  );
};

export default SellerDashboard;
