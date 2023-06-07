import React, { useEffect } from "react";
import { Routes, Route } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { getUserInfo, resetState } from "./features/auth/authSlice";
import "./App.css";
import Container from "react-bootstrap/Container";
import Header from "./components/Header/Header";
import Home from "./pages/Home/Home";
import Register from "./pages/Register/Register";
import Login from "./pages/Login/Login";

function App() {
  const dispatch = useDispatch();

  const { userId, userInfo, isLoading, isSuccess, isError, message } =
    useSelector((state) => state.auth);

  useEffect(() => {
    if (userId && !userInfo) {
      dispatch(getUserInfo());
    }

    if (isError) {
      console.error(message);
    }

    dispatch(resetState());
  }, [userId, userInfo, isLoading, isSuccess, isError, message, dispatch]);

  return (
    <>
      <Header />
      <Container className="mt-3">
        <Routes>
          <Route exact path="/" element={<Home />} />
          <Route path="/register" element={<Register />} />
          <Route path="/login" element={<Login />} />
        </Routes>
      </Container>
    </>
  );
}

export default App;
