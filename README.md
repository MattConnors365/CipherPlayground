# 🔐 CipherPlayground

A modern .NET-based playground for experimenting with classical encryption algorithms like Caesar, Vigenère, and beyond. Designed as a modular, testable, and deployable solution — ideal for learning, extending, or showcasing foundational crypto techniques.

> 🧪 Built with C# in .NET 8.  
> 📁 Organized into a Library, CLI Tool, and ASP.NET Core MVC API.  
> 🌍 Deployed to Render: [Live API](https://cipherplayground-api.onrender.com)

---

## 🎯 Purpose

This project provides some different utilities for experimenting with cryptographical ciphers.
It contains:
- a library, which you can reference within your own .NET projects
- an API, opens up the before mentioned library and all its main methods, if you prefer working with APIs instead of referencing another project, or if you just want to play around with this project (you can curl it right now!)
- a CLI interface, 

---

## 🗂️ Project Breakdown

### 🔧 `CipherPlayground.Library`
Core logic for all cipher implementations.

- 🔄 Supports encryption and decryption
- ✨ Easy to extend with new cipher strategies

Current supported ciphers:
- Caesar 
- Vigenère
- A1Z26

### 💻 `CipherPlayground.CLI`
A terminal interface for quick cipher ops:
```bash
./CipherPlayground.CLI caesar --text "HELLO" --key 3
# Output: KHOOR
```
### 🌐 CipherPlayground.API
A simple RESTful API for encrypting/decrypting text online.
Example (Caesar Encrypt):
```bash
curl -X POST https://cipherplayground-api.onrender.com/api/caesar/encrypt \
     -H "Content-Type: application/json" \
     -d '{"text":"HELLO","key":3}'
# Returns: KHOOR
```
---
## 🚀 Getting Started
### 🛠 Prerequisites
- .NET 8 SDK
- (Optional) curl, Postman, or browser for API tests

### 🔄 Build & Run Locally
```bash
# Clone the repo
git clone https://github.com/your-username/CipherPlayground.git
cd CipherPlayground
# Build solution
dotnet build
# Run CLI example
dotnet run --project CipherPlayground.CLI -- caesar -t "HELLO" -k 3
# Run API locally
dotnet run --project CipherPlayground.API
```

---

# 📄 License
MIT License — do whatever you want, just credit me if you end up using the project please.
See [LICENSE](LICENSE.txt) for full details.

---

# 🙋‍♂️ Author
Matt Connors
[GitHub](https://github.com/MattConnors365/)

---

# 💬 Final Notes
If you're a hiring manager or developer reviewing this repo:
- Check out the API deployment (it works!)
- Browse the core library — it's clean and expandable
- Feedback or ideas? Open an issue or ping me
