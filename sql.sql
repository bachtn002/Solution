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
su.ShopId=15 and su.RoleId=2;



select count(su.UserId) from T_Shop as s, T_ShopUser as su
where s.ShopId=su.ShopId and su.RoleId=2 and s.ShopId=22;



SELECT * FROM T_User as u 
join T_ShopUser as su on u.UserId=su.UserId
join T_Shop as s on su.ShopId=s.ShopId
join T_Role as r on su.RoleId= r.RoleId
join TM_ShopStatus as tm on s.ShopStatusId=tm.ShopStatusId
where u.UserId=56 AND s.IsDelete=0 


















