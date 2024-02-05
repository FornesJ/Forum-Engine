import { ListGroup } from "react-bootstrap";
import { useGetUser } from "../api/api";

type CommentProp = {
    title: string,
    content: string
    userId: number
}

export function Comment({title, content, userId}: CommentProp) {
    const user = useGetUser(userId);
    return (
        <>
            <section className="fw-bold">
                {user.firstName} {user.lastName}
            </section>
            {title}
            <section>
                {content}
            </section>
        </>
    );
}