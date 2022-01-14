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
$status = isset($_POST["status"]) && !empty($_POST["status"]) ? $_POST["status"] : "";

$result = $order->GetOrderInfo($userID, $status);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $result,
    "message" => "Order Information"
);

echo json_encode($array);
