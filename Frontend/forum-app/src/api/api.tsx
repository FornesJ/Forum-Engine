import axios from "axios";
import { useState } from "react";

export const GetPosts = () => {
    const [posts, setPosts] = useState([]);
    axios.get('http://localhost:8000/api/Post').then(res => {
        setPosts(res.data);
    }).catch(err => {
        console.log(err)
    })
    return posts;
}

type GetPostProps = {
    id: number
}

export const GetPostById = ({id}: GetPostProps) => {
    axios.get(`http://localhost:8000/api/Post/${id}`).then(res => {
        console.log(res);
    }).catch(err => {
        console.log(err)
    })
}