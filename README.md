# Library-Management-System
## 简介
    基于ASP.NET的网页图书馆管理系统。
    环境需求：Visual Studio 2017+ ,ASP.NET,SQL sever 
 ## 功能
 ### 游客和学生功能:
  * 查看书籍信息
  * 预约借书
  * 还书
  * 登陆
### 管理员功能
   * 查看书籍信息
   * 借书
   * 还书
   * 修改和添加书籍
   * 修改和添加学生账号和管理员账号
   * 修改书籍状态
 ## 环境配置
 1. 用Visual studio 打开 LiraryDemo解决方案
 2. 打开PM控制台
 3. 输入cd LibraryDemo 进入项目文件夹
 4. 分别输入update-database -c LibraryDemo.Data.LendingInfoDbContext 和update-database -c LibraryDemo.Data.StudentIdentityDbContext来迁移数据库。
 5. 运行IIS press
 6. 此时会遇到一个错误,无视此错误，点击继续。当网页刷新出来提示错误以后，停止运行后再次运行即可正确运行。
