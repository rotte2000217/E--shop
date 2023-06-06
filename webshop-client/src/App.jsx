import { Routes, Route } from "react-router-dom";
import "./App.css";
import Container from "react-bootstrap/Container";
import Header from "./components/Header/Header";
import Home from "./pages/Home/Home";

function App() {
  return (
    <>
      <Header />
      <Container>
        <Routes>
          <Route exact path="/" element={<Home />} />
        </Routes>
      </Container>
    </>
  );
}

export default App;
