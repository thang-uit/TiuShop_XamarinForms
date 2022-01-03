<?php
date_default_timezone_set('Asia/Ho_Chi_Minh');

// $DBHost = "localhost";
// $DBUser = "root";
// $DBPass = "";
// $DBName = "tiushop";

// $con = mysqli_connect($DBHost, $DBUser, $DBPass, $DBName);

// if($con)
// {
//     // echo "Connect Success!";
//     mysqli_query($con, "SET NAMES 'utf8'");
// }
// else
// {
//     echo "Connect Fail!";
//     echo mysqli_connect_error();
// }

class Database
{
    private $servername = "localhost";
    private $username = "root";
    private $password = "";
    private $database = "tiushop";
    private $conn;

    public function connect()
    {
        $this->conn = null;
        try {
            $this->conn = new PDO("mysql:host=" . $this->servername . "; dbname=" . $this->database . "", $this->username, $this->password);
            // set the PDO error mode to exception
            $this->conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
            // echo "Connected successfully";
        } catch (PDOException $e) {
            echo "Connection failed: " . $e->getMessage();
        }

        return $this->conn;
    }
}
