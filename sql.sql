use QL_CTV;
select max(UserId) from T_User;


select u.UserId, s.Name, u.FullName, s.Description, r.RoleName, tms.ShopStatusName, s.CreatedUtcDate 
from   T_User as u, T_ShopUser as su, T_Shop as s, T_Role as r,TM_ShopStatus as tms
where u.UserId=su.UserId and su.ShopId=s.ShopId 
and su.RoleId=r.RoleId and s.ShopStatusId=tms.ShopStatusId and u.UserId=15;


select u.FullName, u.Mobile, tmus.UserStatusName, tmg.GenderName, u.DateOfBirth, 
r.RoleName, su.CreatedUtcDate
from T_User as u, T_ShopUser as su, T_Shop as s, T_Role as r, 
TM_UserStatus as tmus, TM_Gender as tmg
where u.UserId=su.UserId and su.ShopId=s.ShopId 
and su.RoleId=r.RoleId and u.UserStatusId=tmus.UserStatusId
and u.GenderId=tmg.GenderId
and s.ShopId=15 and r.RoleId=2;

select * from T_ShopUser as su , T_Role as r, T_User as u, TM_Gender as g, TM_UserStatus as tmus
where su.UserId=u.UserId and su.RoleId=r.RoleId and u.GenderId=g.GenderId 
and u.UserStatusId= tmus.UserStatusId and
su.ShopId=15 and su.RoleId=2
