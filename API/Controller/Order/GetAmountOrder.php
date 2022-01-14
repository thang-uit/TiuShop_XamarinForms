<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Order.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$order = new Order($db->connect());

$userID = isset($_POST["userID"]) && !empty($_POST["userID"]) ? $_POST["userID"] : "";

$result = $order->GetAmountOrder($userID);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $result,
    "message" => "Amount Order"
);

echo json_encode($array);
