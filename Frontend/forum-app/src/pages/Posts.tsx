import { Button, Container } from "react-bootstrap";
import { useGetPosts } from "../api/api";
import { Post } from "../components/Post";


export function Posts() {
    const posts = useGetPosts();
    return (
        <>
            <Container>
                <h1>Posts</h1>
                {posts.map(p =>
                <Post key={p['id']} id={p['id']} title={p['title']} content={p['content']} userId={p['userId']} />
                )}
            </Container>
        </>
    );
}