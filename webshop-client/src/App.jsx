import { Routes, Route } from "react-router-dom";
import "./App.css";
import Container from "react-bootstrap/Container";
import Header from "./components/Header/Header";
import Home from "./pages/Home/Home";
import Register from "./pages/Register/Register";

function App() {
  return (
    <>
      <Header />
      <Container className="mt-3">
        <Routes>
          <Route exact path="/" element={<Home />} />
          <Route path="/register" element={<Register />} />
        </Routes>
      </Container>
    </>
  );
}

export default App;
