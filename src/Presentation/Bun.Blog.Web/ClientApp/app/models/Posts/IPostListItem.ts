import { ICodeNameGroup } from '../common/ICodeNameGroup'

export interface IPostListItem {
    id: number,
    title: string,
    excerpt: string,
    visits: number,
    category: ICodeNameGroup
}
