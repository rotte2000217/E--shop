import React, { useEffect } from "react";
import Spinner from "react-bootstrap/Spinner";
import LoginForm from "../../components/User/LoginForm";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { loginUser, resetState } from "../../features/auth/authSlice";
import { loginRequestDto } from "../../models/loginDto";
import { notifyError } from "../../utils/notify";

const Login = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const authState = useSelector((state) => state.auth);

  useEffect(() => {
    if (authState.isError) {
      notifyError(authState.message);
    }

    if (authState.isSuccess && authState.userInfo) {
      navigate("/dashboard");
    }

    dispatch(resetState());
  }, [authState, navigate, dispatch]);

  const handleLogin = (data) => {
    const dto = loginRequestDto(data);
    dispatch(loginUser(dto));
  };

  if (authState.isLoading) {
    return (
      <Spinner animation="border" role="status">
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    );
  }

  return (
    <div className="mb-5">
      <h1>Login</h1>
      <LoginForm handleLogin={handleLogin} />
    </div>
  );
};

export default Login;
