$(() => {
    $("#my-body").on('click','.clk', function () {
        const Idd = $(this).data('like');
        const Id = {
            iD: Idd
        }
        
        $.post('/home/like', Id, function (p) {
            Reload();
        })

    })
    function Reload() {
        $("#my-body .well").remove();
        $.get(`/home/getusers`, function (usr) {

            usr.forEach(u => {
                let print = `<div class="well">
                        <a href="/home/enlarge?Id=${u.id}" target="_blank">
                            <img src="${u.url}" style="width:250px; height:250px" />
                        </a>
                        <h4>Posted by ${u.name}</h4>`;
                if (u.able === true) {
                    print += `<div class="row"><div class=col-md-1><button data-like="${u.id}" class="btn btn-primary"><i class="fas fa-thumbs-up"></i>Like</button></div>
                        <div class="col-md-4"><h6>${u.likes} People liked this</h6></div></div>
                    </div> <br/>`
                }
                else {
                    print += `<div class="row"><div class=col-md-1><button data-like="${u.id}" class="btn btn-primary clk"><i class="fas fa-thumbs-up"></i>Like</button></div>
                        <div class="col-md-4"><h6>${u.likes} People liked this</h6></div></div>
                    </div ><br/>`
                }
                $("#my-body").append(

                   print
                )
                
            })
        })
    }
    Reload();
    setInterval(Reload, 30000);
})