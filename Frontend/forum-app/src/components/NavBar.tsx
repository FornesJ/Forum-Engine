import { Container, Nav, Navbar as NavBarBs } from "react-bootstrap";
import { NavLink } from "react-router-dom";

export function NavBar() {
    return (
        <NavBarBs sticky="top" className="bg-dark shadow mb-3">
            <Container>
                <Nav className="me-auto">
                    <Nav.Link to="/" as={NavLink} className="text-white">
                        Home
                    </Nav.Link>
                    <Nav.Link to="/Posts" as={NavLink} className="text-white">
                        Posts
                    </Nav.Link>
                    <Nav.Link to="/About" as={NavLink} className="text-white">
                        About
                    </Nav.Link>
                </Nav>
            </Container>
        </NavBarBs>

    );
}