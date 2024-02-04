import { Card } from "react-bootstrap";
import { GetPosts } from "../api/api";

type PostProps = {
    id: number,
    title: string,
    content: string
}

export function Post({id, title, content}: PostProps) {
    return (
        <Card>
            <Card.Header>

            </Card.Header>
            <Card.Body>

            </Card.Body>
        </Card>
    )
}