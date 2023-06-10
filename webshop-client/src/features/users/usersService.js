import axios from "axios";

const getUsers = async () => {
  const res = await axios.get(`${process.env.REACT_APP_API_URL}/api/users`);

  return res.data;
};

const usersService = {
  getUsers,
};

export default usersService;
