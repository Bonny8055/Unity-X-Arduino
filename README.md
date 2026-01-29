# 🎮 Unity × Arduino Integration

Build interactive **Unity projects powered by real-world Arduino hardware**. This repository is a practical guide for connecting Unity with Arduino to control physical components like **servo motors and LEDs** based on in-game events.

Whether you're building a **portfolio project**, a **VR interaction demo**, or an **IoT-style experience**, this repo gives you a clean starting point.

---

## ✨ What This Project Does

* Control **Arduino components** using **Unity game logic**
* Trigger **real-world actions** when a player reaches a destination in Unity
* Demonstrates **Unity ↔ Serial Communication ↔ Arduino** workflow
* Designed for **portfolio showcases** and learning purposes

---

## 🧠 Use Case Example

> A player moves using **WASD + Mouse** in a Unity scene.
> When the player enters a specific zone (destination point), a **physical flag connected to a servo motor rises** beside your PC.

Virtual intent ➜ Physical response 🚀

---

## 🔧 Pre-Requisites

### 🖥️ Software

1. **Arduino IDE**
2. **Unity** (Recommended: LTS version)
3. **Visual Studio Code**

### 🔌 Hardware

1. **Arduino UNO**
2. **Jumper Wires**
3. **Servo Motor / LEDs** *(based on your output requirement)*
4. USB Cable

---

## ⚙️ Project Workflow

### 🔹 Step 1: Arduino Setup

* Open **Arduino IDE**
* Upload the provided **C++ code** to the Arduino UNO
* After uploading, **note the COM Port** (example: `COM4`)

---

### 🔹 Step 2: Unity Project Setup

* Create a **new Unity project**
* This project uses **VR Core** for the final output (optional for non-VR use)

---

### 🔹 Step 3: Script Configuration

* Attach the **PlayerControl** script to the **Player GameObject**
* Attach the **GameManager** script to an **Empty GameObject**
* Update the **Serial Port** in the script

```csharp
string portName = "COM4"; // Replace with your port
```

* Set your **Destination Point** in the Inspector

---

### 🔹 Step 4: Hardware Connections

Connect the servo motor to the Arduino UNO as follows:

| Wire Color | Connection   |
| ---------- | ------------ |
| 🟠 Orange  | A9 (Signal)  |
| 🔴 Red     | 5V (Voltage) |
| 🟤 Brown   | GND (Ground) |

📌 *Refer to the wiring image provided in this repository for clarity.*

---

## ▶️ Run the Project

1. Connect **Arduino UNO** to your PC
2. Ensure the **correct COM port** is set
3. Click **Play** in Unity
4. Move the player into the destination zone

💥 BOOM! Your **physical output reacts instantly** 🎉

---

## 📂 Repository Structure

```
Unity-X-Arduino/
│── ArduinoCode/
│   └── Arduino.ino
│── UnityProject/
│   ├── Scripts/
│   │   ├── PlayerControl.cs
│   │   └── GameManager.cs
│── Wiring/
│   └── circuit_diagram.png
│── README.md
```

---

## 🚀 Applications

* AR / VR Physical Interaction Demos
* Smart Environment Prototypes
* Hardware + Game Engine Experiments
* Portfolio Projects for XR / Unity Developers

---

## 🛠️ Troubleshooting

* ❌ Servo not moving? → Check **COM port** & **baud rate**
* ❌ No serial data? → Close Arduino Serial Monitor
* ❌ Unity not responding? → Ensure Arduino is connected **before Play**

---

## 📌 Future Enhancements

* Multiple hardware outputs
* Sensor-based input (Ultrasonic, IR, etc.)
* Wireless communication (Bluetooth / WiFi)
* VR hand gesture-based triggers

---

## 🤝 Contributing

Contributions, improvements, and experiments are welcome.
Feel free to fork this repository and build your own extensions.

---

## ⭐ Support

If this project helped you:

* Star ⭐ the repository
* Share it with fellow Unity / Arduino developers

Happy Building 🔧🎮
