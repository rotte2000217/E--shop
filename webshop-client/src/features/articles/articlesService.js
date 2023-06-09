import axios from "axios";

const getArticles = async (sellerId) => {
  const queryParameters = sellerId ? `?sellerId=${sellerId}` : "";

  const res = await axios.get(
    `${process.env.REACT_APP_API_URL}/api/articles${queryParameters}`
  );

  return res.data;
};

const addArticle = async (accessToken, articleDto) => {
  const config = {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  };

  const res = await axios.post(
    `${process.env.REACT_APP_API_URL}/api/articles`,
    articleDto,
    config
  );

  return res.data;
};

const articlesService = {
  getArticles,
  addArticle,
};

export default articlesService;
