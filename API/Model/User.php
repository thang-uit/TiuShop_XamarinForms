<?php
class Account
{
    public $conn;

    public $userID;
    public $name;
    public $email;
    public $phone;
    public $address;

    public function __construct($database)
    {
        $this->conn = $database;
    }
}
