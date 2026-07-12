<p align="center">
  <img src="logo.png" width="120" alt="Power Plan Tweak logo" />
</p>

<h1 align="center">Power Plan Tweak</h1>

<p align="center">
  A Windows tool that safely unlocks hidden power-plan settings and tunes CPU boost performance,
  with automatic backup and rollback if anything goes wrong.
</p>

---

Power Plan Tweak là công cụ Windows giúp mở khoá các tuỳ chọn Power Plan (Power Options) đang bị
ẩn và tinh chỉnh hiệu năng CPU (boost mode, min/max processor state) một cách an toàn — có backup
tự động trước khi đổi và tự rollback nếu có bước nào thất bại.

## Quy trình 5 tầng

1. **An toàn** — Tạo System Restore Point + backup power scheme hiện tại ra file `.pow`.
2. **Nhận diện** — Phát hiện máy là laptop/desktop, có pin hay không, để quyết định có áp dụng
   tweak cho nguồn pin (DC) hay không.
3. **Mở khoá** — Quét toàn bộ registry power settings, gỡ cờ ẩn cho mọi setting đang bị ẩn (kể cả
   setting OEM thêm vào).
4. **Thực thi** — Set Max processor state + Boost Mode (chọn được `Disabled` / `Enabled` /
   `Aggressive` / `Efficient Enabled` / `Efficient Aggressive`) cho AC, và một cấu hình an toàn hơn
   cho DC (pin). Bất kỳ lệnh `powercfg` nào thất bại sẽ lập tức rollback lại toàn bộ thay đổi.
5. **Nghiệm thu** — Chạy stress test ngắn, so sánh `% Processor Performance` lúc rảnh và lúc tải để
   xác nhận boost có thực sự hoạt động đúng với mode đã chọn hay không, chỉ ghi log — không rollback.

## Yêu cầu

- Windows 10 phiên bản 1809 (Build 17763) trở lên, hoặc Windows 11.
- Quyền Administrator (tool tự relaunch qua UAC nếu chưa có).
- .NET Framework 4.8 — nếu máy chưa có, tool tự tải bộ cài offline chính thức từ Microsoft và cài
  âm thầm trước khi tiếp tục.

## Build từ source

```
dotnet build -c Release
```

Release build gộp `Guna.UI2.dll` thẳng vào exe (qua ILRepack), nên `bin\Release\` chỉ còn đúng
1 file `.exe` để phân phối — không kèm DLL, không kèm file cấu hình.

**Lưu ý quan trọng:** `Guna.UI2.WinForms` (thư viện UI dùng trong project) là thư viện **thương
mại**, cấp phép theo license riêng cho từng developer (có bản dùng thử 14 ngày), **không** đi kèm
trong repo này (`packages/` đã bị gitignore). Muốn build lại từ source, bạn cần tự restore package
`Guna.UI2.WinForms` phiên bản `2.0.4.8` bằng license/trial của riêng bạn. Chi tiết xem
[THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md).

## Cấu trúc project

```
Core/               Toàn bộ logic (elevation, .NET check, power plan engine, CPU stress test...)
frm_PowerPlanTweak  Form chính (log, progress bar, chọn Boost Mode, nút Run)
frmSupport          Dialog xin hỗ trợ khi đóng app (donation QR, "Don't show again")
Assets/             Ảnh QR donate (nhúng vào exe qua Embedded Resource)
Resources/app.ico   Icon của app
tools/ILRepack/     Công cụ merge assembly cho Release build (Apache-2.0, đã kèm sẵn)
```

## Feedback

[facebook.com/PycCodeTools](https://www.facebook.com/PycCodeTools)

## License

Source code của project này theo giấy phép [MIT](LICENSE). Các thư viện phụ thuộc (Guna.UI2.WinForms,
ILRepack) có license riêng — xem [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md).
