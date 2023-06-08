import React from "react";
import Spinner from "react-bootstrap/Spinner";
import UserDetails from "../../components/User/UserDetails";
import { useSelector } from "react-redux";

const Dashboard = () => {
  const { userInfo } = useSelector((state) => state.auth);

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
