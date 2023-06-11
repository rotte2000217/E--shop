import React from "react";
import { statusIdToName } from "../../models/verificationStatus";
import "../../style/User.css";

const UserItem = ({ userData }) => {
  return (
    <div className="user">
      <div className="user-photo">
        <img
          src={
            userData.photo ||
            `${process.env.REACT_APP_PLACEHOLDER_API}/400x400?text=No+Image`
          }
          alt="user"
        />
      </div>

      <div className="user-middle">
        <h5 className="user-title">
          {userData.firstName} {userData.lastName}
        </h5>
        <div className="user-details">
          <div>Email: {userData.email}</div>
          <div>Username: {userData.username}</div>
        </div>
      </div>

      <div className="user-right">
        <div className={`user-status status-${userData.verificationStatus}`}>
          Status: {statusIdToName(userData.verificationStatus)}
        </div>
        <div className="user-id">ID: {userData.id}</div>
      </div>
    </div>
  );
};

export default UserItem;
