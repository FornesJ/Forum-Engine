import { Button } from "react-bootstrap";
import { GetPosts } from "../api/api";
import { Post } from "../components/Post";


export function Posts() {
    const tb = () => {
        const posts = GetPosts();
    }
    return (
        <>
            <h1>Posts:</h1>
            <Button onClick={tb}>click</Button>
        </>
    );
}