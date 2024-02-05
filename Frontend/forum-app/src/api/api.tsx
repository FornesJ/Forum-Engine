import axios from "axios";
import React from "react";
import { useState } from "react";

export const useGetPosts = () => {
    const [posts, setPosts] = useState([]);

    React.useEffect(() => {
        axios.get('http://localhost:8000/api/Post').then(res => {
            setPosts(res.data);
        }).catch(err => {
            console.log(err);
        });
    }, []);
    return posts;
}

export const useGetUser = (id: number) => {
    const [user, setUser] = useState(Object);

    React.useEffect(() => {
        axios.get(`http://localhost:8000/api/User/${id}`).then(res => {
            setUser(res.data);
        }).catch(err => {
            console.log(err);
        });
    }, []);

    return user;
}

export const useGetCommentToPost = (postId: number) => {
    const [comment, setComment] = useState([]);

    React.useEffect(() => {
        axios.get(`http://localhost:8000/api/Post/${postId}/Comments`).then(res => {
            setComment(res.data);
        }).catch(err => {
            console.log(err);
        });
    }, []);

    return comment;
}