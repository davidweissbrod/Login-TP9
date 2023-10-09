using System.Data.SqlClient;
using Dapper;

public class BD{
    private static string _connectionString = @"Server=localhost; DataBase=LoginBD; Trusted_Connection = True;";

    public static List<Usuario> LevantarUsuarios(){
        using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql = "SELECT * FROM Usuarios";
            listaUsuarios = db.Query<Usuario>(sql).ToList();
        }
        return listaUsuarios;
    }

    public static Usuario IniciarSesion(int idUs){
        Usuario us = null;
        using(SqlConnection db = new SqlConnection(_connectionString)){
            string sql = "SELECT idUsuario FROM Usuarios WHERE idUsuario = @pidUsuario";    
            us = db.QueryFirstOrDefault<Usuario>(sql, new {pidUsuario = idUs});
        }
        return us;
    }

    public static Usuario AñadirUsuario(Usuario user){
        string sql = "INSERT INTO Usuarios(idUsuario, username, contraseña, nombre, apellido, mail) VALUES(@pidUsario, @pusername, @pcontraseña, @pnombre, @papellido, @pmail)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pidUsuario = user.idUsuario, pusername = user.username, pcontraseña = user.contraseña, pnombre = user.nombre, papellido = user.apellido, pmail = user.mail});
        }
        return user;
    }

    public static string CambiarContraseña(string nuevaContraseña){
        string sql = "UPDATE Usuarios SET contraseña = @nuevaContraseña WHERE contraseña = @pcontraseña";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pcontraseña = nuevaContraseña});
        }
        return nuevaContraseña;
    }
}
