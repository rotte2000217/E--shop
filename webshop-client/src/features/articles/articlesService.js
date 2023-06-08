import axios from "axios";

const getArticles = async (sellerId) => {
  const queryParameters = sellerId ? `?sellerId=${sellerId}` : "";

  const res = await axios.get(
    `${process.env.REACT_APP_API_URL}/api/articles${queryParameters}`
  );

  return res.data;
};

const articlesService = {
  getArticles,
};

export default articlesService;
