import axios from "axios";

const getOrders = async (buyerId) => {
  const queryParameters = buyerId ? `?buyerId=${buyerId}` : "";

  const res = await axios.get(
    `${process.env.REACT_APP_API_URL}/api/orders${queryParameters}`
  );

  return res.data;
};

const ordersService = {
  getOrders,
};

export default ordersService;
