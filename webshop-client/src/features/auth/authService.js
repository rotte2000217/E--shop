import axios from "axios";

const registerUser = async (userData) => {
  const res = await axios.post(
    `${process.env.REACT_APP_API_URL}/api/users`,
    userData
  );

  return res.data;
};

const authService = {
  registerUser,
};

export default authService;
