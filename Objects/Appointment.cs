using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Appointment
  {
    private int _id;
    private int _clientId;
    private int _stylistId;
    private DateTime _dateAndTime;
    private int _durationMinutes;

    public Appointment (int clientId, int stylistId, DateTime dateAndTime, int durationMinutes, int id)
    {
      _id = id;
      _clientId = clientId;
      _stylistId = stylistId;
      _dateAndTime = dateAndTime;
      _durationMinutes = durationMinutes;
    }

    public int GetId()
    {
      return _id;
    }
    public int GetClientId()
    {
      return _clientId;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public DateTime GetDate()
    {
      return _dateAndTime;
    }
    public int GetDuration()
    {
      return _durationMinutes;
    }

    public void SetClientId(int clientId)
    {
      _clientId = clientId;
    }
    public void SetStylistId(int stylistId)
    {
      _stylistId = stylistId;
    }
    public void SetDate(DateTime date)
    {
      _dateAndTime = date;
    }
    public void SetDuration(int duration)
    {
      _durationMinutes = duration;
    }

    public static List<Appointment> GetAll()
    {
      List<Appointment> allAppointments = new List<Appointment> {};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM appointments;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int appointmentId = rdr.GetInt32(0);
        int clientId = rdr.GetInt32(1);
        int stylistId = rdr.GetInt32(2);
        DateTime dateAndTime = rdr.GetDateTime(3);
        int duration = rdr.GetInt32(4);

        Appointment newAppointment = new Appointment(clientId, stylistId, dateAndTime, duration, appointmentId);
        allAppointments.Add(newAppointment);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allAppointments;
    }
  }
}
