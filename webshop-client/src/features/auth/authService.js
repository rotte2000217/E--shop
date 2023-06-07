import axios from "axios";

const registerUser = async (registerDto) => {
  const res = await axios.post(
    `${process.env.REACT_APP_API_URL}/api/users`,
    registerDto
  );

  return res.data;
};

const loginUser = async (loginDto) => {
  const res = await axios.post(
    `${process.env.REACT_APP_API_URL}/api/users/login`,
    loginDto
  );

  return res.data;
};

const authService = {
  registerUser,
  loginUser,
};

export default authService;
