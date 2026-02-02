using UnityEngine;
using System.IO.Ports;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Serial Port Settings")]
    [SerializeField] private string portName = "COM6";
    [SerializeField] private int baudRate = 9600;

    [Header("Game Settings")]
    [SerializeField] private Transform endPoint;
    [SerializeField] private float triggerDistance = 1f;

    [Header("Potentiometer Data")]
    public int potValue = 0;          // Raw value (0–1023)
    public float potNormalized = 0f;  // Normalized value (0–1)

    private SerialPort serialPort;
    private PlayerMovement playerMovement;
    private bool hasReachedEnd = false;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        InitializeSerialPort();
    }

    void InitializeSerialPort()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.ReadTimeout = 5;
            serialPort.Open();
            Debug.Log($"Serial port {portName} opened.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Serial port error: {e.Message}");
        }
    }

    void Update()
    {
        ReadSerialData();

        if (playerMovement == null || endPoint == null || hasReachedEnd)
            return;

        float distance = Vector3.Distance(
            playerMovement.transform.position,
            endPoint.position
        );

        if (distance <= triggerDistance)
        {
            PlayerReachedEnd();
        }
    }

    void ReadSerialData()
    {
        if (serialPort == null || !serialPort.IsOpen)
            return;

        try
        {
            string data = serialPort.ReadLine();

            // Expecting: POT:512
            if (data.StartsWith("POT:"))
            {
                string valueString = data.Substring(4);

                if (int.TryParse(valueString, out int value))
                {
                    potValue = value;
                    potNormalized = potValue / 1023f;

                    // Debug.Log($"Potentiometer: {potValue}");
                }
            }
        }
        catch { } // ignore timeout errors
    }

    void PlayerReachedEnd()
    {
        hasReachedEnd = true;

        if (playerMovement != null)
        {
            playerMovement.UnlockCursor();
            playerMovement.enabled = false;
        }

        SendToArduino('H');
        Debug.Log("End reached. Sent H to Arduino.");
    }

    void SendToArduino(char command)
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                serialPort.Write(command.ToString());
            }
            catch (Exception e)
            {
                Debug.LogError($"Send failed: {e.Message}");
            }
        }
    }

    public void SendCustomCommand(char command)
    {
        SendToArduino(command);
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Serial port closed.");
        }
    }

    void OnDestroy()
    {
        OnApplicationQuit();
    }

    void OnDrawGizmos()
    {
        if (endPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(endPoint.position, triggerDistance);
        }
    }
}
