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
import BuyerDashboard from "../../components/Layout/BuyerDashboard";
import { orderQueryParameters } from "../../models/queryParameters";

const Dashboard = () => {
  const dispatch = useDispatch();

  const { userId, userInfo } = useSelector((state) => state.auth);

  useEffect(() => {
    if (userInfo) {
      let queryParams;
      switch (userInfo.role) {
        case UserRole.Buyer:
          queryParams = orderQueryParameters({
            buyerId: userId,
          });
          dispatch(getOrders(queryParams)).then((_) =>
            dispatch(ordersSlice.actions.resetState())
          );
          dispatch(getArticles()).then((_) =>
            dispatch(articlesSlice.actions.resetState())
          );
          break;
        case UserRole.Seller:
          queryParams = orderQueryParameters({
            sellerId: userId,
          });
          dispatch(getOrders(queryParams)).then((_) =>
            dispatch(ordersSlice.actions.resetState())
          );
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

  return userInfo.role === UserRole.Buyer ? (
    <BuyerDashboard>{profileComponent}</BuyerDashboard>
  ) : userInfo.role === UserRole.Seller ? (
    <SellerDashboard>{profileComponent}</SellerDashboard>
  ) : (
    profileComponent
  );
};

export default Dashboard;
