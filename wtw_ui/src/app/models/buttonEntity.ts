import { Observable } from "rxjs";
import { ResultEntity } from "./resultEntity";

export class ButtonEntity {
    name: string;
    class: string;
    actionResult: Observable<ResultEntity>;
    actionResultList: Observable<ResultEntity[]>;
    postAction: any;
    messageOk: string;
    messageError: string;
    consoleLogFail: string;
}