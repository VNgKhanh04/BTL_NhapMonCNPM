$(document).ready(function () {
    const audioPlayer = document.getElementById('audioPlayer');
    const musicPlayer = document.getElementById('musicPlayer');
    const playPauseBtn = document.getElementById('playPauseBtn');
    const playPauseIcon = document.getElementById('playPauseIcon');
    const prevBtn = document.getElementById('prevBtn');
    const nextBtn = document.getElementById('nextBtn');
    const volumeControl = document.getElementById('volumeControl');
    const songProgress = document.getElementById('songProgress');
    const currentTime = document.getElementById('currentTime');
    const totalTime = document.getElementById('totalTime');
    const currentSongTitle = document.getElementById('currentSongTitle');
    const currentSongArtist = document.getElementById('currentSongArtist');
    const currentSongImage = document.getElementById('currentSongImage');
    const closePlayerBtn = document.getElementById('closePlayerBtn');
    
    // Khởi tạo danh sách phát
    let playlist = [];
    let currentSongIndex = 0;
    
    // Thu thập thông tin bài hát từ các button phát nhạc
    $('.play-button').each(function() {
        playlist.push({
            url: $(this).data('song-url'),
            title: $(this).data('song-title'),
            artist: 'Unknown', // Có thể thêm thông tin nghệ sĩ nếu có
        });
    });
    
    // Xử lý sự kiện click vào nút phát nhạc
    $('.play-button').on('click', function() {
        const songUrl = $(this).data('song-url');
        const songTitle = $(this).data('song-title');
        
        // Tìm index của bài hát trong playlist
        currentSongIndex = playlist.findIndex(song => song.url === songUrl);
        
        // Phát nhạc
        playSong(songUrl, songTitle, 'Unknown');
        
        // Hiển thị thanh phát nhạc
        $(musicPlayer).addClass('show');
    });
    
    // Hàm phát nhạc
    function playSong(url, title, artist) {
        // Cập nhật thông tin bài hát
        currentSongTitle.textContent = title;
        currentSongArtist.textContent = artist;
        
        // Cập nhật đường dẫn audio
        audioPlayer.src = url;
        audioPlayer.load();
        
        // Phát nhạc
        audioPlayer.play()
            .then(() => {
                playPauseIcon.classList.remove('fa-play');
                playPauseIcon.classList.add('fa-pause');
            })
            .catch(error => {
                console.error('Error playing audio:', error);
            });
    }
    
    // Xử lý sự kiện play/pause
    playPauseBtn.addEventListener('click', function() {
        if (audioPlayer.paused) {
            audioPlayer.play();
            playPauseIcon.classList.remove('fa-play');
            playPauseIcon.classList.add('fa-pause');
        } else {
            audioPlayer.pause();
            playPauseIcon.classList.remove('fa-pause');
            playPauseIcon.classList.add('fa-play');
        }
    });
    
    // Xử lý sự kiện prev
    prevBtn.addEventListener('click', function() {
        if (playlist.length === 0) return;
        
        currentSongIndex = (currentSongIndex - 1 + playlist.length) % playlist.length;
        const song = playlist[currentSongIndex];
        playSong(song.url, song.title, song.artist);
    });
    
    // Xử lý sự kiện next
    nextBtn.addEventListener('click', function() {
        if (playlist.length === 0) return;
        
        currentSongIndex = (currentSongIndex + 1) % playlist.length;
        const song = playlist[currentSongIndex];
        playSong(song.url, song.title, song.artist);
    });
    
    // Xử lý sự kiện volume change
    volumeControl.addEventListener('input', function() {
        audioPlayer.volume = this.value;
    });
    
    // Cập nhật thanh progress và thời gian
    audioPlayer.addEventListener('timeupdate', function() {
        const progress = (audioPlayer.currentTime / audioPlayer.duration) * 100;
        songProgress.style.width = `${progress}%`;
        
        // Cập nhật thời gian hiện tại
        const curMinutes = Math.floor(audioPlayer.currentTime / 60);
        const curSeconds = Math.floor(audioPlayer.currentTime % 60);
        currentTime.textContent = `${curMinutes}:${curSeconds < 10 ? '0' + curSeconds : curSeconds}`;
        
        // Cập nhật tổng thời gian nếu có
        if (!isNaN(audioPlayer.duration)) {
            const totalMinutes = Math.floor(audioPlayer.duration / 60);
            const totalSeconds = Math.floor(audioPlayer.duration % 60);
            totalTime.textContent = `${totalMinutes}:${totalSeconds < 10 ? '0' + totalSeconds : totalSeconds}`;
        }
    });
    
    // Xử lý sự kiện kết thúc bài hát
    audioPlayer.addEventListener('ended', function() {
        // Chuyển sang bài tiếp theo
        nextBtn.click();
    });
    
    // Xử lý sự kiện đóng player
    closePlayerBtn.addEventListener('click', function() {
        $(musicPlayer).removeClass('show');
        audioPlayer.pause();
        playPauseIcon.classList.remove('fa-pause');
        playPauseIcon.classList.add('fa-play');
    });

    
});

$(document).ready(function() {
    function getParameterByName(name) {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get(name);
    }

    
    const musicId = getParameterByName('id');
    
    $("#submitComment").click(function() {
        sendComment();
    });
    
    $("#commentText").keypress(function(e) {
        if (e.which === 13 && !e.shiftKey) {
            e.preventDefault();
            sendComment();
        }
    });
    
    function sendComment() {
        const commentText = $("#commentText").val().trim();
        
        if (!commentText) {
            alert("Vui lòng nhập nội dung bình luận!");
            return;
        }
        
        $.ajax({
            url: "/addComment",
            method: "POST",
            data: {
                baiHatId: musicId,
                commentText: commentText
            },
            success: function(response) {
                if (response.success) {
                    $("#commentText").val("");
                    
                    $("#noComments").hide();
                    
                    const newComment = `
                        <div class="comment-item p-3 mb-3 new-comment" id="comment-${response.newComment.id}">
                            <div class="d-flex">
                                <div class="flex-grow-1 text-light">
                                    <div class="d-flex justify-content-between align-items-center">
                                        <h6 class="mb-0">User</h6>
                                        <small class="text-muted">${response.newComment.time}</small>
                                    </div>
                                    <div class="comment-text mt-2">
                                        <p>${response.newComment.content}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `;
                    
                    $("#commentsList").prepend(newComment);
                    
                    setTimeout(() => {
                        $(".new-comment").removeClass('new-comment');
                    }, 3000);
                    
                    Toast.fire({
                        icon: 'success',
                        title: 'Bình luận của bạn đã được gửi thành công.'
                    });
                } else {
                    Toast.fire({
                        icon: 'info',
                        title: response.message
                    });
                }
            },
            error: function(xhr, status, error) {
                Toast.fire({
                    icon: 'error',
                    title: 'Có lỗi xảy ra khi gửi bình luận. Vui lòng thử lại sau.'
                })
                console.error(error);
            }
        });
    }
});