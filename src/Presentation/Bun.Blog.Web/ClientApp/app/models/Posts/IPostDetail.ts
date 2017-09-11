import { IUser } from "../Users/User";

export interface IPostDetail {
    id: number,
    title: string,
    content: string,
    author: IUser
}
