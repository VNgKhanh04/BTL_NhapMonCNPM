# Nhóm G14
## Cách cài đặt Project
### Yêu cầu hệ thống
- [.NET 7.0](https://dotnet.microsoft.com/download/dotnet/7.0) hoặc mới hơn
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (hoặc SQL Server Express)
- [Git](https://git-scm.com/downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) hoặc [Visual Studio Code](https://code.visualstudio.com/)
- #### Clone Project 
```bash
git clone https://github.com/VNgKhanh04/BTL_NhapMonCNPM.git
```
- #### Khôi phục các gói NuGet
```bash
dotnet restore
```
- #### Chạy file tạo database : BTL_NMCNPM.sql
- #### Cấu hình cơ sở dữ liệu
    - Mở file appsetting.json và cập nhật
```bash 
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=your_db;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
- #### Chạy ứng dụng
```bash
dotnet run 
#hoặc
dotnet watch run
```
