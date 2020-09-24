import { ICampus } from 'src/app/shared/models/ICampus';
import { IDegree } from 'src/app/shared/models/IDegree';
import { IDepartment } from 'src/app/shared/models/IDepartment';

export interface IUserCamPusAndDepartmentAndDegree{
    campuses:ICampus[];
    departments:IDepartment[];
    degrees:IDegree[];

}