import React from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { Formik } from "formik";
import "../../style/Form.css";
import { verificationSchema } from "../../models/verificationSchema";
import {
  VerificationStatus,
  statusIdToName,
  statusNameToId,
} from "../../models/verificationStatus";

const VerificationForm = ({ handleSubmit, users }) => {
  return (
    <Formik
      initialValues={{
        userId: "",
        verificationStatus: "",
      }}
      validationSchema={verificationSchema}
      onSubmit={(values, { resetForm }) => {
        handleSubmit(values);
        resetForm();
      }}
    >
      {({
        values,
        errors,
        touched,
        handleChange,
        handleBlur,
        handleSubmit,
      }) => (
        <Form onSubmit={handleSubmit}>
          <Form.Group className="mb-3" controlId="userId">
            <Form.Label>User</Form.Label>
            <Form.Select
              name="userId"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.userId}
              className={`border border-2 ${
                touched.userId
                  ? errors.userId
                    ? "border-danger"
                    : "border-success"
                  : ""
              }`}
            >
              <option value="">Select User</option>
              {users.map((user) => (
                <option key={user.id} value={user.id}>
                  {user.username} ({statusIdToName(user.verificationStatus)})
                </option>
              ))}
            </Form.Select>
            {errors.userId && touched.userId ? (
              <div className="error-message">{errors.userId}</div>
            ) : null}
          </Form.Group>

          <Form.Group className="mb-3" controlId="verificationStatus">
            <Form.Label>Status</Form.Label>
            <Form.Select
              name="verificationStatus"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.verificationStatus}
              className={`border border-2 ${
                touched.verificationStatus
                  ? errors.verificationStatus
                    ? "border-danger"
                    : "border-success"
                  : ""
              }`}
            >
              <option value="">Verify as</option>
              {Object.keys(VerificationStatus).map((statusName) => (
                <option
                  key={statusNameToId(statusName)}
                  value={statusNameToId(statusName)}
                >
                  {statusName}
                </option>
              ))}
            </Form.Select>
            {errors.verificationStatus && touched.verificationStatus ? (
              <div className="error-message">{errors.verificationStatus}</div>
            ) : null}
          </Form.Group>

          <Button variant="primary" type="submit">
            Submit
          </Button>
        </Form>
      )}
    </Formik>
  );
};

export default VerificationForm;
