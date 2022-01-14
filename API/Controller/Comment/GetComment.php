<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Comment.php');
include_once('../../Config/Common.php');

// $_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$comment = new Comment($db->connect());

$productID = isset($_GET["productID"]) && !empty($_GET["productID"]) ? $_GET["productID"] : "";

$arrayComment = $comment->GetComment($productID);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arrayComment,
    "message" => "Get comment"
);

echo json_encode($array);
