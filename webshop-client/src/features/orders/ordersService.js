import axios from "axios";

const getOrders = async (buyerId) => {
  const queryParameters = buyerId ? `?buyerId=${buyerId}` : "";

  const res = await axios.get(
    `${process.env.REACT_APP_API_URL}/api/orders${queryParameters}`
  );

  return res.data;
};

const cancelOrder = async (accessToken, orderId) => {
  const config = {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  };

  const res = await axios.delete(
    `${process.env.REACT_APP_API_URL}/api/orders/${orderId}`,
    config
  );

  return res.data;
};

const ordersService = {
  getOrders,
  cancelOrder,
};

export default ordersService;
