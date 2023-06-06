import React from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";

const RegisterForm = () => {
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(e);
  };

  return (
    <Form onSubmit={handleSubmit}>
      <Form.Group className="mb-3" controlId="username">
        <Form.Label>Username</Form.Label>
        <Form.Control
          type="text"
          name="username"
          placeholder="Enter username"
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="email">
        <Form.Label>Email address</Form.Label>
        <Form.Control
          type="email"
          name="email"
          placeholder="Enter your email address"
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="password">
        <Form.Label>Password</Form.Label>
        <Form.Control
          type="password"
          name="password"
          placeholder="Enter password"
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="confirmPassword">
        <Form.Label>Confirm Password</Form.Label>
        <Form.Control
          type="password"
          name="confirmPassword"
          placeholder="Confirm password"
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="firstName">
        <Form.Label>First Name</Form.Label>
        <Form.Control
          type="text"
          name="firstName"
          placeholder="Enter your first name"
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="lastName">
        <Form.Label>Last Name</Form.Label>
        <Form.Control
          type="text"
          name="lastName"
          placeholder="Enter your last name"
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="birthdate">
        <Form.Label>Date of Birth</Form.Label>
        <Form.Control
          type="date"
          name="birthdate"
          placeholder="Enter your date of birth"
        />
      </Form.Group>

      <Form.Group className="mb-3" controlId="address">
        <Form.Label>Address</Form.Label>
        <Form.Control
          type="text"
          name="address"
          placeholder="Enter your address"
        />
      </Form.Group>

      <Button variant="primary" type="submit">
        Register
      </Button>
    </Form>
  );
};

export default RegisterForm;
