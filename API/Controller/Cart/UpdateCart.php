<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Cart.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$cart = new Cart($db->connect());

$cartID = isset($_POST["cartID"]) && !empty($_POST["cartID"]) ? $_POST["cartID"] : "";
$quantity = isset($_POST["quantity"]) && !empty($_POST["quantity"]) ? $_POST["quantity"] : "1";

$status = $cart->UpdateCart($cartID, $quantity);

$array = [];

if ($status == 1) {
    $array = array(
        "status" => $SUCCESS,
        "message" => "Update cart successfully!"
    );
} else {
    $array = array(
        "status" => $FAIL,
        "message" => "Update cart fail!"
    );
}

echo json_encode($array);
