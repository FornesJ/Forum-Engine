import { Button, Container } from "react-bootstrap";
import Icons  from "../data/icons.json";
import { Link } from "react-router-dom";

export function Footer() {
    return (
        <footer className="fixed-bottom text-center bg-dark" color="white">
            <div className='text-center p-3 text-white'>
                Follow us at:
            </div>
            <Container className="p-4">
                <section className="mb-4">
                    {Icons.map(icon => (
                        <Link className='rounded-circle m-3 hover-overlay'
                        key={icon.id} to={""}                        >
                            <img alt="icon" src={icon.url} style={{width: "3rem", height: "3rem"}}/>
                        </Link>
                    ))}
                </section>
            </Container>
            <div className='text-center p-3 text-white' style={{ backgroundColor: 'rgba(0, 0, 0, 0.2)' }}>
                @ Forum Page
            </div>
        </footer>
    );
};