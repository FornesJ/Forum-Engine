import { Button, Card, ListGroup } from "react-bootstrap";
import { useGetCommentToPost, useGetUser } from "../api/api";
import { Comment } from "./Comment";
import { useState } from "react";

type PostProps = {
    id: number,
    title: string,
    content: string
    userId: number
}

export function Post({id, title, content, userId}: PostProps) {
    const [show, setShow] = useState(false);

    const user = useGetUser(userId);
    const comments = useGetCommentToPost(id);

    return (
        <Card className="m-5">
            <Card.Header className="">
                <Card.Text className="fw-bold">{user.firstName} {user.lastName}</Card.Text>
                {title}
            </Card.Header>
            <Card.Body>
                {content}
            </Card.Body>
            <ListGroup>
                {!show && (<Button onClick={() => setShow(true)} variant="light">Show comments</Button>)}
                {show && comments.map(c =>
                    <>
                        <ListGroup.Item>
                            <Comment key={c['id']} title={c['title']} content={c['content']} userId={c['userId']} />
                        </ListGroup.Item>
                    </>
                )}
                {show && (<Button onClick={() => setShow(false)} variant="secondary">Hide comments</Button>)}
            </ListGroup>
        </Card>
    )
}