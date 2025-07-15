import { ButtonEntity } from "./buttonEntity";

export class MessageEntity {
    message: string;
    type: string;
    class: string;
    classButton: string;
    actions: ButtonEntity[];
}