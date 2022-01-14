<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Order.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$order = new Order($db->connect());

$orderID = isset($_POST["orderID"]) && !empty($_POST["orderID"]) ? $_POST["orderID"] : "";
$status = isset($_POST["status"]) && !empty($_POST["status"]) ? $_POST["status"] : "";

$result = $order->UpdateOrderStatus($orderID, $status);

$array = [];

if ($result == 1) {
    $array = array(
        "status" => $SUCCESS,
        "message" => "Update Order Status Success"
    );
} else {
    $array = array(
        "status" => $FAIL,
        "message" => "Update Order Status Fail"
    );
}

echo json_encode($array);
