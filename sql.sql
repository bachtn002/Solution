use QL_CTV;
select max(UserId) from T_User;


select u.UserId, s.Name, u.FullName, s.Description, r.RoleName, tms.ShopStatusName, s.CreatedUtcDate 
from   T_User as u, T_ShopUser as su, T_Shop as s, T_Role as r,TM_ShopStatus as tms
where u.UserId=su.UserId and su.ShopId=s.ShopId 
and su.RoleId=r.RoleId and s.ShopStatusId=tms.ShopStatusId and u.UserId=15