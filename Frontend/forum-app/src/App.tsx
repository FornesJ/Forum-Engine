import { Routes, Route } from "react-router-dom";
import { Container } from "react-bootstrap";
import { Home } from "./pages/Home";
import { Posts } from "./pages/Posts";
import { NavBar } from "./components/NavBar";
import { About } from "./pages/About";

export default function App() {
  return (
    <>
      <NavBar />
      <Container className="mb-4">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/Posts" element={<Posts />} />
          <Route path="/About" element={<About />} />
        </Routes>
      </Container>
    </>
  );
}
