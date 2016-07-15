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

    public Appointment (int clientId, int stylistId, DateTime dateAndTime, int durationMinutes, int id =0)
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

    public override bool Equals(System.Object otherAppointment)
    {
      if (!(otherAppointment is Appointment))
      {
        return false;
      }
      else
      {
        Appointment newAppointment = (Appointment) otherAppointment;
        bool idEquality = this.GetId() == newAppointment.GetId();
        bool clientIdEquality = this.GetClientId() == newAppointment.GetClientId();
        bool stylistIdEquality = this.GetStylistId() == newAppointment.GetStylistId();
        bool dateEquality = this.GetDate() == newAppointment.GetDate();
        bool durationEquality = this.GetDuration() == newAppointment.GetDuration();
        return (idEquality && clientIdEquality && stylistIdEquality && dateEquality && durationEquality);
      }
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO appointments (client_id, stylist_id, date_and_time, duration) OUTPUT INSERTED.id VALUES (@ClientId, @StylistId, @DateAndTime, @Duration);", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = this.GetClientId();
      cmd.Parameters.Add(clientIdParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetStylistId();
      cmd.Parameters.Add(stylistIdParameter);

      SqlParameter dateAndTimeParameter = new SqlParameter();
      dateAndTimeParameter.ParameterName = "@DateAndTime";
      dateAndTimeParameter.Value = this.GetDate();
      cmd.Parameters.Add(dateAndTimeParameter);

      SqlParameter durationParameter = new SqlParameter();
      durationParameter.ParameterName = "@Duration";
      durationParameter.Value = this.GetDuration();
      cmd.Parameters.Add(durationParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM appointments;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
