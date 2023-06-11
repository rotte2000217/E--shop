import React, { useEffect } from "react";
import OrderList from "../Orders/OrderList";
import { useDispatch, useSelector } from "react-redux";
import VerificationForm from "../User/VerificationForm";
import { UserRole } from "../../models/userRole";
import { verifyUser, resetState } from "../../features/users/usersSlice";
import { verificationRequestDto } from "../../models/verificationDto";
import { notifySuccess, notifyError } from "../../utils/notify";
import UserList from "../User/UserList";

const AdminDashboard = ({ children }) => {
  const dispatch = useDispatch();

  const { orders } = useSelector((state) => state.orders);
  const { users, isSuccess, isLoading, isError, message } = useSelector(
    (state) => state.users
  );

  useEffect(() => {
    if (isError && message) {
      notifyError(message);
    }

    if (isSuccess && message) {
      notifySuccess(message);
    }

    dispatch(resetState());
  }, [isSuccess, isLoading, isError, message, dispatch]);

  const handleVerify = (data) => {
    const thunkData = {
      userId: data.userId,
      verificationDto: verificationRequestDto(data),
    };

    dispatch(verifyUser(thunkData));
  };

  return (
    <div>
      <h1>Admin Dashboard</h1>
      <hr />
      {children}
      <hr />
      <div>
        <h3>All Orders</h3>
        <OrderList orders={orders} />
      </div>
      <hr />
      <div>
        <h3>Sellers</h3>
        <UserList
          users={users.filter((user) => user.role === UserRole.Seller)}
        />
      </div>
      <hr />
      <div>
        <h3>Verify Users</h3>
        <VerificationForm
          handleSubmit={handleVerify}
          users={users.filter((user) => user.role === UserRole.Seller)}
        />
      </div>
    </div>
  );
};

export default AdminDashboard;
