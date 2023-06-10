import React, { useEffect } from "react";
import Spinner from "react-bootstrap/Spinner";
import UserDetails from "../../components/User/UserDetails";
import { useSelector, useDispatch } from "react-redux";
import {
  getArticles,
  articlesSlice,
} from "../../features/articles/articlesSlice";
import { getOrders, ordersSlice } from "../../features/orders/ordersSlice";
import { UserRole } from "../../models/userRole";
import SellerDashboard from "../../components/Layout/SellerDashboard";

const Dashboard = () => {
  const dispatch = useDispatch();

  const { userId, userInfo } = useSelector((state) => state.auth);

  useEffect(() => {
    if (userInfo) {
      switch (userInfo.role) {
        case UserRole.Buyer:
          dispatch(getOrders(userId)).then((_) =>
            dispatch(ordersSlice.actions.resetState())
          );
          break;
        case UserRole.Seller:
          dispatch(getArticles(userId)).then((_) =>
            dispatch(articlesSlice.actions.resetState())
          );
          break;
        default:
          break;
      }
    }
  }, [userInfo, userId, dispatch]);

  if (!userInfo) {
    return (
      <Spinner animation="border" role="status">
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    );
  }

  const profileComponent = (
    <div>
      <h3>Profile</h3>
      <UserDetails userData={userInfo} />
    </div>
  );

  return userInfo.role === UserRole.Seller ? (
    <SellerDashboard>{profileComponent}</SellerDashboard>
  ) : (
    profileComponent
  );
};

export default Dashboard;
