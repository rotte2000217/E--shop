import React, { useEffect } from "react";
import Spinner from "react-bootstrap/Spinner";
import UserDetails from "../../components/User/UserDetails";
import { useSelector, useDispatch } from "react-redux";
import { getArticles, resetState } from "../../features/articles/articlesSlice";
import { UserRole } from "../../models/userRole";

const Dashboard = () => {
  const dispatch = useDispatch();

  const { userId, userInfo } = useSelector((state) => state.auth);

  useEffect(() => {
    if (userInfo && userInfo.role === UserRole.Seller) {
      dispatch(getArticles(userId)).then((_) => dispatch(resetState()));
    }
  }, [userInfo, userId, dispatch]);

  if (!userInfo) {
    return (
      <Spinner animation="border" role="status">
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    );
  }

  return (
    <div>
      <h1>Dashboard</h1>
      <hr />
      <div>
        <h3>Profile</h3>
        <UserDetails userData={userInfo} />
      </div>
    </div>
  );
};

export default Dashboard;
